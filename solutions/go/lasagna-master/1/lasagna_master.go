package lasagna

func PreparationTime(layers []string, averageLayerPreparationTime int) int {
	if averageLayerPreparationTime == 0 {
		averageLayerPreparationTime = 2
	}

	return len(layers) * averageLayerPreparationTime
}

func Quantities(layers []string) (int, float64) {
	noodels := 0
	sauce := 0.0

	for _, layer := range layers {
		if layer == "noodles" {
			noodels += 50
		} else if layer == "sauce" {
			sauce += 0.2
		}
	}

	return noodels, sauce
}

func AddSecretIngredient(friend, mine []string) {
	mine[len(mine) -1] = friend[len(friend) -1]
}

func ScaleRecipe(quantities []float64, peopleCount int) []float64{
	var scaled []float64
	scale := float64(peopleCount) / 2

	for i := range quantities{
		scaled = append(scaled, quantities[i] * scale)
	}

	return scaled
}
