class Triangle(val a: Double, val b: Double, val c: Double) {

    constructor(a: Int, b: Int, c: Int) : this(a.toDouble(), b.toDouble(), c.toDouble())

    init {
        require(a > 0.0 && b > 0.0 && c > 0.0)
        require(a + b >= c && b + c >= a && c + a >= b)
    }

    val isEquilateral: Boolean = a == b && b == c
    val isIsosceles: Boolean = a == b || b == c || a == c
    val isScalene: Boolean = a != b && b != c && b != a
}
