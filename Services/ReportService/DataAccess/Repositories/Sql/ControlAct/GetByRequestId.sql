SELECT id AS "Id",
       request_id AS "RequestId",
       work_time AS "WorkTime",
       address AS "Address",
       has_commuting_device AS "HasCommutingDevice",
       has_violation AS "HasViolation",
       metering_device_location_type AS "MeteringDeviceLocationType",
       metering_device_location AS "MeteringDeviceLocation",
       device_readings AS "DeviceReadings",
       workers AS "Workers"
FROM control_acts
WHERE request_id = @RequestId
       