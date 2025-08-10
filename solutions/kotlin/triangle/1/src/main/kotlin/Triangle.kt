class Triangle<out T : Number> {
    constructor(a:T,b:T,c:T){
    }

    val isEquilateral: Boolean = a == b && b == c
    val isIsosceles: Boolean =  a == b || b == c || a == c
    val isScalene: Boolean = a != b && b != c && b != a

    fun checkTriangle(){
        if((a == 0 || b == 0 || c == 0)||(a + b))
    }
}
