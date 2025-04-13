SELECT id AS "Id",
       city AS "City",
       street AS "Street",
       house AS "House",
       room AS "Room",
       flat AS "Flat",
       device AS "Device",
       status AS "status",
       type AS type,
       creation_time AS "CreationTime",
       work_time AS "WorkTime",
       completing_time AS "CompletionTime",
       brigade_id AS "BrigadeId",
       geotag AS "GeoTag",
       before_immger AS "BefireImage",
       after_image AS "AfterImage"
FROM requests
WHERE id = @Id