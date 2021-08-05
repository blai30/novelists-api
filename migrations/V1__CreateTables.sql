CREATE TABLE users
(
    id           uuid                     DEFAULT uuid_generate_v4() NOT NULL
        CONSTRAINT users_pk
            PRIMARY KEY,
    email        VARCHAR                                             NOT NULL,
    password     VARCHAR                                             NOT NULL,
    display_name VARCHAR                                             NOT NULL,
    created_at   TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at   TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

CREATE UNIQUE INDEX users_email_uindex
    ON users (email);

CREATE TABLE publications
(
    id         uuid                     DEFAULT uuid_generate_v4() NOT NULL
        CONSTRAINT publications_pk
            PRIMARY KEY,
    user_id    uuid                                                NOT NULL
        CONSTRAINT publications_users_id_fk
            REFERENCES users
            ON UPDATE CASCADE ON DELETE CASCADE,
    title      VARCHAR                                             NOT NULL,
    synopsis   VARCHAR                                             NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE chapters
(
    id             uuid                     DEFAULT uuid_generate_v4() NOT NULL
        CONSTRAINT chapters_pk
            PRIMARY KEY,
    publication_id uuid                                                NOT NULL
        CONSTRAINT chapters_publications_id_fk
            REFERENCES publications
            ON UPDATE CASCADE ON DELETE CASCADE,
    index          INTEGER                                             NOT NULL,
    title          VARCHAR                                             NOT NULL,
    body           VARCHAR                                             NOT NULL,
    created_at     TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at     TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE reviews
(
    id         uuid                     DEFAULT uuid_generate_v4() NOT NULL
        CONSTRAINT reviews_pk
            PRIMARY KEY,
    chapter_id uuid                                                NOT NULL
        CONSTRAINT reviews_chapters_id_fk
            REFERENCES chapters
            ON UPDATE CASCADE ON DELETE CASCADE,
    user_id    uuid                                                NOT NULL
        CONSTRAINT reviews_users_id_fk
            REFERENCES users
            ON UPDATE CASCADE ON DELETE CASCADE,
    title      VARCHAR                                             NOT NULL,
    body       VARCHAR                                             NOT NULL,
    rating     SMALLINT                                            NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

CREATE TRIGGER on_update_users
    BEFORE UPDATE
    ON users
    FOR EACH ROW
EXECUTE PROCEDURE set_updated_at();

CREATE TRIGGER on_update_publications
    BEFORE UPDATE
    ON publications
    FOR EACH ROW
EXECUTE PROCEDURE set_updated_at();

CREATE TRIGGER on_update_chapters
    BEFORE UPDATE
    ON chapters
    FOR EACH ROW
EXECUTE PROCEDURE set_updated_at();

CREATE TRIGGER on_update_reviews
    BEFORE UPDATE
    ON reviews
    FOR EACH ROW
EXECUTE PROCEDURE set_updated_at();
