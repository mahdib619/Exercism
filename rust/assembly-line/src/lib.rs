const MIN_CARS_PER_HOUR : f64 = 221.0;

pub fn production_rate_per_hour(speed: u8) -> f64 {
    speed as f64 * MIN_CARS_PER_HOUR * success_rate(speed)
}

pub fn working_items_per_minute(speed: u8) -> u32 {
    (production_rate_per_hour(speed) / 60.0) as u32
}

fn success_rate(speed: u8) -> f64 {
    if speed < 5 {
        1.0
    } else if speed < 9 {
        0.9
    } else {
        0.77
    }
}
