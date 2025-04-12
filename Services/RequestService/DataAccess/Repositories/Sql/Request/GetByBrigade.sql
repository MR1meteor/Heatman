SELECT id AS "Id",
       address AS "Address",
       device AS "Device",
       status AS "Status",
       type AS "Type",
       creation_time AS "CreationTime",
       work_time AS "WorkTime",
       completion_time AS "CompletionTime",
       brigade_id AS "BrigadeId"
FROM requests
WHERE brigade_id = @BrigadeId