package cars

const (
	SingleCarCost   = 10000
	TenCarGroupCost = 95000
)

func CalculateWorkingCarsPerHour(productionRate int, successRate float64) float64 {
	return float64(productionRate) * (successRate / 100)
}

func CalculateWorkingCarsPerMinute(productionRate int, successRate float64) int {
	return int(CalculateWorkingCarsPerHour(productionRate, successRate) / 60)
}

func CalculateCost(carsCount int) uint {
	groupsOfTenCost := (carsCount / 10) * TenCarGroupCost
	singleCarsCost := (carsCount % 10) * SingleCarCost

	return uint(groupsOfTenCost + singleCarsCost)
}
