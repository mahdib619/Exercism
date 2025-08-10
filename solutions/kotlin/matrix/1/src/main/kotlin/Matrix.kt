class Matrix(private val matrixAsString: String) {

    fun column(colNr: Int): List<Int> = matrixAsString.split('\n').map { it.split(Regex(" +")).elementAt(colNr - 1) }.map { it.toInt() }

    fun row(rowNr: Int): List<Int> = matrixAsString.split('\n').elementAt(rowNr - 1).split(Regex(" +")).map { it.toInt() }
}
