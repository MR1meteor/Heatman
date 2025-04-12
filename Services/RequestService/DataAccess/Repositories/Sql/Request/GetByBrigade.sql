SELECT id AS "Id",
       city AS "City",
       street AS "Street",
       room AS "Room",
       flat AS "Flat",
       device AS "Device",
       status AS "Status",
       type AS "Type",
       creation_time AS "CreationTime",
       work_time AS "WorkTime",
       completion_time AS "CompletionTime",
       brigade_id AS "BrigadeId",
       geotag AS "GeoTag"
FROM requests
WHERE brigade_id = @BrigadeId