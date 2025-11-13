CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20251111131439_Initial') THEN
    CREATE TABLE resumes (
        id uuid NOT NULL,
        introduction text NOT NULL,
        full_name_first_name text NOT NULL,
        full_name_last_name text NOT NULL,
        address_street text NOT NULL,
        address_zip_code text NOT NULL,
        address_city text NOT NULL,
        email text NOT NULL,
        git_hub_link text NOT NULL,
        linked_in_link text NOT NULL,
        photo bytea NOT NULL,
        created timestamp with time zone NOT NULL,
        last_modified timestamp with time zone NOT NULL,
        CONSTRAINT pk_resumes PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20251111131439_Initial') THEN
    CREATE TABLE projects (
        id uuid NOT NULL,
        resume_id uuid NOT NULL,
        project_title text NOT NULL,
        description text NOT NULL,
        project_url text NOT NULL,
        demo_gif bytea NOT NULL,
        is_featured boolean NOT NULL,
        created timestamp with time zone NOT NULL,
        last_modified timestamp with time zone NOT NULL,
        CONSTRAINT pk_projects PRIMARY KEY (id),
        CONSTRAINT fk_projects_resumes_resume_id FOREIGN KEY (resume_id) REFERENCES resumes (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20251111131439_Initial') THEN
    CREATE INDEX ix_projects_resume_id ON projects (resume_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "migration_id" = '20251111131439_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
    VALUES ('20251111131439_Initial', '9.0.9');
    END IF;
END $EF$;
COMMIT;

