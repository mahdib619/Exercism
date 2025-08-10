include("permutations.jl")

function solve(str)
    letters = unique(collect(replace(str, r"\W" => "")))

    lr = split(str, " == ")
    fl = Set(map(first, vcat(split(lr[1], " + "), lr[2])))

    exprarr = []
    for exp in split(str, "==")
        nums, pn = [], 1
        push!(exprarr, nums)
        for c in reverse(collect(exp))
            if isletter(c)
                push!(nums, "$pn*$c")
                pn *= 10
            elseif c != ' '
                pn = 1
            end
        end
    end
    expr = join(join.(exprarr, "+"), "==")

    eval(quote
        function dosolve($(Symbol.(letters)...))
            $(Meta.parse(expr))
        end
    end)

    for combo in permutations(0:9, length(letters))
        av = Dict([a => b for (a, b) in zip(letters, combo)])
        any(k -> k[2] == 0 && in(k[1], fl), av) && continue
        Base.invokelatest(dosolve, map(l->av[l], letters)...) && return av
    end
end