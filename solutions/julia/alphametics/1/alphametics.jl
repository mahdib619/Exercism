include("permutations.jl")

function solve(str)
    combos::Vector{Any}, letters = repeat([nothing], 10), unique(collect(replace(str, r"\W" => "")))
    for i in eachindex(letters)
        combos[i] = letters[i]
    end

    lr = split(str, " == ")
    lefts = split(lr[1], " + ")

    for combo in permutations(combos)
        global dict = Dict([i => c for (i, c) in zip(combo, 0:9)])
        try
		    sum(map(getnum, lefts)) == getnum(lr[2]) && (haskey(dict, nothing) && pop!(dict, nothing); return dict)
        catch
            continue
        end
    end
end

getnum(s) = (n = join(map(c -> dict[c], collect(s))); n[1]=='0' ? throw(ErrorException("")) : parse(Int, n))