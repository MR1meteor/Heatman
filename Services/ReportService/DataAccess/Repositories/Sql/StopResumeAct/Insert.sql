INSERT INTO stop_resume_acts(request_id, type, work_time, address, has_commuting_device, result, work_method, metering_device_location_type, metering_device_location, device_readings, work_method_type, workers, client_full_name)
VALUES(@RequestId, @Type, @WorkTime, @Address, @HasCommutingDevice, @Result, @WorkMethod, @MeteringDeviceLocationType, @MeteringDeviceLocation, @DeviceReadings, @WorkMethodType, @Workers, @ClientFullName)