//Package weather provides info about weather.
package weather

//CurrentCondition repersent current condition of weather.
var CurrentCondition string

//CurrentLocation repersent location of weather condition.
var CurrentLocation string

//Forecast returns condition of weather.
func Forecast(city, condition string) string {
	CurrentLocation, CurrentCondition = city, condition
	return CurrentLocation + " - current weather condition: " + CurrentCondition
}
