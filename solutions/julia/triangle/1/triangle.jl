is_equilateral(sides) = is_triangle(sides) && sides[1] == sides[2] == sides[3]
is_isosceles(sides) = is_triangle(sides) && sides[1] == sides[2] || sides[1] == sides[3] || sides[2] == sides[3]
is_scalene(sides) = is_triangle(sides) && sides[1] != sides[2] && sides[1] != sides[3] && sides[2] != sides[3]
is_triangle(sides) = all(s -> s!=0, sides) && sides[1] + sides[2] >= sides[3] && sides[1] + sides[3] >= sides[2] && sides[2] + sides[3] >= sides[1]