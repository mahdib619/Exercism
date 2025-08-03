class Squares(val n: Int) {
    fun sumOfSquares() = (n * (n + 1) * (2 * n + 1)) / 6

    fun squareOfSum() = Math.pow(sumOfFirstNNumbers().toDouble(), 2.0).toInt()

    fun difference() = squareOfSum() - sumOfSquares()

    fun sumOfFirstNNumbers() = (n * (n + 1)) / 2
}
