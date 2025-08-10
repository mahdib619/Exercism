#[derive(Clone, PartialEq, Eq, Debug)]
pub enum LogLevel {
    Info,
    Warning,
    Error,
    Debug,
}

pub fn log(level: LogLevel, message: &str) -> String {
    match level {
        LogLevel::Info => info(message),
        LogLevel::Warning => warn(message),
        LogLevel::Error => error(message),
        LogLevel::Debug => debug(message)
    }
}
pub fn info(message: &str) -> String {
    format_level("INFO") + message
}
pub fn warn(message: &str) -> String {
    format_level("WARNING") + message
}
pub fn error(message: &str) -> String {
    format_level("ERROR") + message
}
pub fn debug(message: &str) -> String{
    format_level("DEBUG") + message
}

fn format_level(level: &str) -> String {
    format!("[{}]: ", level)
}
