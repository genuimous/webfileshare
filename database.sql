/* Tables */
-- parts
create table dbo.parts
(
  id bigint identity not null,
  visible_name nvarchar(100) not null,
  owner nvarchar(50) not null,
  passphrase nvarchar(100) not null,
  path nvarchar(1000) not null
)

alter table dbo.parts add constraint pk_parts primary key(id)
create unique index ui_parts_owner on dbo.parts(owner)

-- directories
create table dbo.directories
(
  id bigint identity not null,
  part_id bigint not null,
  visible_name nvarchar(100) not null,
  psk nvarchar(100) not null,
  directory nvarchar(100) not null
)

alter table dbo.directories add constraint pk_directories primary key(id)
alter table dbo.directories add constraint fk_directories_part foreign key(part_id) references dbo.parts(id)
create unique index ui_directories_key on dbo.directories(psk)
create unique index ui_directories_identity on dbo.directories(part_id, directory)

/* Procedures */
-- raise_custom_error
create procedure raise_custom_error
  @error_text varchar(1000)
as
begin
  declare @error_message varchar(2047)

  set @error_message = '{' + @error_text + '}'

  raiserror(@error_message, 16, 1)
end

-- create_directory
create procedure create_directory
  @psk nvarchar(100) output,
  @owner nvarchar(50),
  @directory nvarchar(100),
  @visible_name nvarchar(100)
as
begin
  set nocount on
  set xact_abort on

  declare @part_id bigint

  select @part_id = id
  from dbo.parts
  where owner = @owner

  if @part_id is not null
  begin
    set @psk = newid()

    begin transaction

    insert into dbo.directories
    (
      part_id,
      visible_name,
      psk, 
      directory
    )
    values
    (
      @part_id,
      @visible_name,
      @psk, 
      @directory
    )

    commit transaction
  end
  else
  begin
    exec dbo.raise_custom_error 'Не найден владелец!'
  end
end

-- edit_directory
create procedure edit_directory
  @owner nvarchar(50),
  @directory nvarchar(100),
  @visible_name nvarchar(100)
as
begin
  set nocount on
  set xact_abort on

  declare @part_id bigint

  select @part_id = id
  from dbo.parts
  where owner = @owner

  if @part_id is not null
  begin
    begin transaction

    update dbo.directories
    set visible_name = @visible_name
    where part_id = @part_id and directory = @directory

    commit transaction
  end
  else
  begin
    exec dbo.raise_custom_error 'Не найден владелец!'
  end
end

-- delete_directory
create procedure delete_directory
  @owner nvarchar(50),
  @directory nvarchar(100)
as
begin
  set nocount on
  set xact_abort on

  declare @part_id bigint

  select @part_id = id
  from dbo.parts
  where owner = @owner

  if @part_id is not null
  begin
    begin transaction

    delete from dbo.directories
    where part_id = @part_id and directory = @directory

    commit transaction
  end
  else
  begin
    exec dbo.raise_custom_error 'Не найден владелец!'
  end
end

-- change_key
create procedure change_key
  @psk nvarchar(100) output,
  @owner nvarchar(50),
  @directory nvarchar(100)
as
begin
  set nocount on
  set xact_abort on

  declare @part_id bigint

  select @part_id = id
  from dbo.parts
  where owner = @owner

  if @part_id is not null
  begin
    set @psk = newid()

    begin transaction

    update dbo.directories
    set psk = @psk
    where part_id = @part_id and directory = @directory

    commit transaction
  end
  else
  begin
    exec dbo.raise_custom_error 'Не найден владелец!'
  end
end

/* Functions */
-- available_directories
create function available_directories
(
  @owner nvarchar(50)
)
returns @result table
(
  visible_name nvarchar(100),
  psk nvarchar(100),
  directory nvarchar(100)
)
as
begin
  insert into @result
  (
    visible_name,
    psk,
    directory
  )
  select
    d.visible_name,
    d.psk,
    d.directory
  from
    dbo.directories d 
    join dbo.parts p on p.id = d.part_id
  where
    p.owner = @owner
  order by
    d.directory

  return
end