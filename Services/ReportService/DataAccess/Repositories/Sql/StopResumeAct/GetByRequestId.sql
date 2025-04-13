SELECT id AS "Id",
       request_id AS "RequestId",
       type AS "Type",
       work_time AS "WorkTime",
       address AS "Address",
       has_commuting_device AS "HasCommutingDevice",
       result AS "Result",
       work_method AS "WorkMethod",
       metering_device_location_type AS "MeteringDeviceLocationType",
       metering_device_location AS "MeteringsDevieLocation",
       device_readings AS "DeviceReadings",
       work_method_type AS "WorkMethodType",
       workers AS "Workers",
       client_full_name AS "ClientFullName"
FROM stop_resume_acts
WHERE request_id = @RequestId