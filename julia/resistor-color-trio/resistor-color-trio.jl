colorvalues = Dict("black" => 0,"brown" => 1,"red" => 2,"orange" => 3,"yellow" => 4,"green" => 5,"blue" => 6,"violet" => 7,"grey" => 8,"white" => 9)

function label(colors)
    (a, b, (z,v)) = map(c -> colorvalues[c], colors) .|> [identity, e -> e == 0 ? "" : e, c -> (c += colors[2] == "black" ? 1 : 0; c >= 3 ? ('0'^(c-3), "kilo") : ('0'^c,""))]
	return "$(a)$(b)$(z) $(v)ohms"
end