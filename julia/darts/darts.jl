function score(x, y)
    d = √((-x)^2 + (-y)^2)
	d <= 1 && return 10
	d <= 5 && return 5
	d <= 10 && return 1
	return 0
end