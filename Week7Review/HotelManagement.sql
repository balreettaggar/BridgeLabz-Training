-- Database
CREATE DATABASE HotelReservation;

-- Schemas

CREATE SCHEMA reservation;
CREATE SCHEMA billing;

-- Tables

CREATE TABLE reservation.hotels (
    hotel_id SERIAL PRIMARY KEY,
    hotel_name VARCHAR(50) NOT NULL,
    hotel_location VARCHAR(50) NOT NULL
);

CREATE TABLE reservation.room_types (
    room_type_id SERIAL PRIMARY KEY,
    room_type_name VARCHAR(50) NOT NULL,
    room_type_price NUMERIC(8,2) NOT NULL
);

CREATE TABLE reservation.rooms (
    room_id SERIAL PRIMARY KEY,
    room_number VARCHAR(25) NOT NULL,
    room_type_id INT NOT NULL,
    hotel_id INT NOT NULL,

    FOREIGN KEY (hotel_id) REFERENCES reservation.hotels(hotel_id),
    FOREIGN KEY (room_type_id) REFERENCES reservation.room_types(room_type_id)
);


CREATE TABLE reservation.customers (
    customer_id SERIAL PRIMARY KEY,
    customer_name VARCHAR(50) NOT NULL,
    customer_email VARCHAR(50) UNIQUE NOT NULL,
    customer_phone VARCHAR(10)
);


CREATE TABLE reservation.reservations (
    reservation_id SERIAL PRIMARY KEY,
    room_id INT NOT NULL,
    customer_id INT NOT NULL,
    hotel_id INT NOT NULL,
    status VARCHAR(20) DEFAULT 'active' CHECK (status IN ('active', 'cancelled')),
    check_in DATE NOT NULL,
    check_out DATE,

    FOREIGN KEY (room_id) REFERENCES reservation.rooms(room_id),
    FOREIGN KEY (customer_id) REFERENCES reservation.customers(customer_id),
    FOREIGN KEY (hotel_id) REFERENCES reservation.hotels(hotel_id),
    CHECK (check_out IS NULL OR check_out > check_in)
);

CREATE TABLE billing.payments (
    payment_id SERIAL PRIMARY KEY,
    payment_date DATE DEFAULT CURRENT_DATE,
    reservation_id INT NOT NULL,
    amount NUMERIC(10,2) NOT NULL,

    FOREIGN KEY (reservation_id) REFERENCES reservation.reservations(reservation_id)
);

-- Data Entry into tables

INSERT INTO reservation.hotels
(hotel_name, hotel_location)
VALUES
('Hotel Grandious', 'Delhi'),
('Taj Hotel', 'Mumbai'),
('The Royal Inn', 'Chandigarh'),
('The Park Hotel', 'Bangalore'),
('Lake View Hotel', 'Amritsar');


INSERT INTO reservation.room_types
(room_type_name, room_type_price)
VALUES
('Single', 1800),
('Double', 2500),
('Family Suite', 4000),
('Deluxe', 5000),
('Luxury Suite', 7000);


INSERT INTO reservation.rooms
(room_number, room_type_id, hotel_id)
VALUES
('101', 1, 1),
('102', 2, 1),
('201', 3, 2),
('301', 4, 3),
('401', 5, 4);


INSERT INTO reservation.customers
(customer_name, customer_email, customer_phone)
VALUES
('Balreet', 'balreetttaggar@gmail.com', '7888802771'),
('Vishvas', 'vishvasvaglay@gmail.com', '9898989898'),
('Harshdeep', 'harshdeep@gmail.com', '8783268239'),
('Karan', 'rahul@gmail.com', '9876543210'),
('Aman', 'aman@gmail.com', '9123456789');


INSERT INTO reservation.reservations
(room_id, customer_id, hotel_id, check_in, check_out, status)
VALUES
(1, 1, 1, '2026-10-01', '2026-10-04', 'active'),
(2, 2, 1, '2026-10-05', '2026-10-08', 'active'),
(3, 3, 2, '2026-10-10', '2026-10-12', 'active'),
(4, 4, 3, '2026-10-15', '2026-10-20', 'active'),
(5, 5, 4, '2026-10-20', '2026-10-22', 'cancelled');


INSERT INTO billing.payments
(reservation_id, amount)
VALUES
(1, 5400),
(2, 7500),
(3, 8000),
(4, 25000),
(5, 14000);


-- Join to display reservation details

SELECT r.reservation_id, c.customer_name, h.hotel_name, rm.room_number, r.check_in, r.check_out, r.status
FROM reservation.reservations r
JOIN reservation.rooms rm ON r.room_id = rm.room_id
JOIN reservation.customers c ON r.customer_id = c.customer_id
JOIN reservation.hotels h ON r.hotel_id = h.hotel_id;


-- cte to count hotel wise occupancy

WITH hotel_occupancy AS (
    SELECT
        hotel_id, COUNT(*) AS total_reservations
    FROM reservation.reservations
    GROUP BY hotel_id
)
SELECT h.hotel_name, ho.total_reservations
FROM hotel_occupancy ho
JOIN reservation.hotels h ON ho.hotel_id = h.hotel_id;


-- subquery to count rooms with booking freq above avg

SELECT
    room_id, COUNT(*) AS booking_count
FROM reservation.reservations
GROUP BY room_id
HAVING COUNT(*) >
(
    SELECT AVG(booking_count)
    FROM
    (
        SELECT room_id, COUNT(*) AS booking_count
        FROM reservation.reservations
        GROUP BY room_id
    ) AS subq
);

-- temp tables for avaailable rooms

CREATE TEMP TABLE available_rooms AS
SELECT room_id, room_number, hotel_id
FROM reservation.rooms
WHERE room_id NOT IN
(
    SELECT room_id
    FROM reservation.reservations
    WHERE status = 'active'
);

SELECT * FROM available_rooms;


-- View for active rooms

CREATE OR REPLACE VIEW reservation.active_reservations1 AS
SELECT r.reservation_id, r.status, r.room_id, h.hotel_name
FROM reservation.reservations r
JOIN reservation.hotels h ON r.hotel_id = h.hotel_id
WHERE r.status = 'active';

SELECT * FROM reservation.active_reservations1;


-- UDF to calculate total billing price

CREATE OR REPLACE FUNCTION booking_cost(p_reservation_id INT)
RETURNS NUMERIC
LANGUAGE plpgsql
AS $$
DECLARE
    price NUMERIC;
    nights INT;
BEGIN
    SELECT rt.room_type_price, r.check_out - r.check_in
    INTO price, nights
    FROM reservation.reservations r
    JOIN reservation.rooms rm ON r.room_id = rm.room_id
    JOIN reservation.room_types rt ON rm.room_type_id = rt.room_type_id
    WHERE r.reservation_id = p_reservation_id;
    RETURN price * nights;
END;
$$;

SELECT booking_cost(1);


-- procedure to make a new entry of reservations

CREATE OR REPLACE PROCEDURE create_reservation(p_room_id INT, p_customer_id INT, p_hotel_id INT,
    p_check_in DATE, p_check_out DATE
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO reservation.reservations
    (room_id, customer_id, hotel_id, check_in, check_out, status)
    VALUES
    (p_room_id, p_customer_id, p_hotel_id, p_check_in, p_check_out, 'active');
END;
$$;

CALL create_reservation(3,2,2,'2026-11-01','2026-11-05');


-- trigger to prevent room overlap 

CREATE OR REPLACE FUNCTION room_overlap()
RETURNS TRIGGER
AS $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM reservation.reservations
        WHERE room_id = NEW.room_id
        AND status = 'active'
        AND NEW.check_in < check_out
        AND NEW.check_out > check_in
    )
    THEN
        RAISE EXCEPTION 'Room already booked';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER prevent_room_overlap
BEFORE INSERT ON reservation.reservations
FOR EACH ROW
EXECUTE FUNCTION room_overlap();


-- cursor for checkout billing

CREATE OR REPLACE PROCEDURE generate_checkout_billing()
LANGUAGE plpgsql
AS $$
DECLARE
    r RECORD;
BEGIN
    FOR r IN
        SELECT reservation_id FROM reservation.reservations
    LOOP
        INSERT INTO billing.payments
        (reservation_id, amount)
        VALUES
        (r.reservation_id, booking_cost(r.reservation_id));
    END LOOP;
END;
$$;

CALL generate_checkout_billing();


-- indexing

CREATE INDEX idx_room_number ON reservation.rooms(room_number);

CREATE INDEX idx_customer_id ON reservation.reservations(customer_id);

CREATE INDEX idx_reservation_dates ON reservation.reservations(check_in, check_out);

SELECT * FROM pg_indexes WHERE schemaname = 'reservation' AND tablename = 'reservations';


-- transaction and row locking

BEGIN;
SELECT *
FROM reservation.rooms
WHERE room_id = 1
FOR UPDATE;
INSERT INTO reservation.reservations
(room_id, customer_id, hotel_id, check_in, check_out, status)
VALUES
(1, 2, 1, '2026-11-01', '2026-11-04', 'active');
COMMIT;

SELECT * FROM reservation.rooms WHERE room_id = 1 FOR UPDATE;


-- User Priveleges

CREATE USER balreet WITH PASSWORD 'balreet123';

GRANT USAGE ON SCHEMA reservation TO balreet;

GRANT USAGE ON SCHEMA billing TO balreet;

GRANT SELECT, INSERT, UPDATE ON ALL TABLES IN SCHEMA reservation TO balreet;

GRANT SELECT, INSERT, UPDATE ON ALL TABLES IN SCHEMA billing TO balreet;

GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA reservation TO balreet;

GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA billing TO balreet;