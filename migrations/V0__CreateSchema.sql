CREATE SCHEMA IF NOT EXISTS novelists;

-- Used to generate uuid for the id primary key column.
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Used to generate new timestamp in the updated_at column when a record is updated.
CREATE OR REPLACE FUNCTION set_updated_at()
    RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;
