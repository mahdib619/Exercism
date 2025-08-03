defmodule FreelancerRates do
  @daily_rate 8.0
  @monthly_billable_days 22

  def daily_rate(hourly_rate),
    do: hourly_rate * @daily_rate

  def apply_discount(before_discount, discount),
    do: before_discount - before_discount * discount / 100

  def monthly_rate(hourly_rate, discount),
    do: ceil(daily_rate(apply_discount(hourly_rate, discount)) * @monthly_billable_days)

  def days_in_budget(budget, hourly_rate, discount),
    do: Float.floor(budget / apply_discount(daily_rate(hourly_rate), discount), 1)
end
