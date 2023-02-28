defmodule Lasagna do
  def expected_minutes_in_oven, do: 40
  def remaining_minutes_in_oven(mins_in_oven), do: expected_minutes_in_oven() - mins_in_oven
  def preparation_time_in_minutes(layers), do: layers * 2
  def total_time_in_minutes(layers, preparation_time), do: preparation_time_in_minutes(layers) + preparation_time
  def alarm, do: "Ding!"
end
