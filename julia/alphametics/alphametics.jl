include("permutations.jl")

function solve(str)
    letters = Dict([c => 0 for c in collect(replace(str, r"\W" => ""))])

    lr = split(str, " == ")
    fl = Set(map(first, vcat(split(lr[1], " + "), lr[2])))

    exprarr = []
    for exp in split(str, "==")
        nums, pn = Dict(), 1
        push!(exprarr, nums)
        for c in reverse(collect(exp))
            if isletter(c)
                nums[c] = get(nums, c, 0) + pn
                pn *= 10
            elseif c != ' '
                pn = 1
            end
        end
    end
    expr = join(join.(map.(kv -> "$(kv[2])*$(kv[1])", collect.(exprarr)), "+"), "==")

    eval(quote
        function dosolve($(Symbol.(keys(letters))...))
            $(Meta.parse(expr))
        end
    end)

    for combo in permutations(0:9, length(letters))
        for ((k, v), i) in zip(letters, eachindex(combo))
            letters[k] = combo[i]
        end

        any(k -> k[2] == 0 && in(k[1], fl), letters) && continue
        Base.invokelatest(dosolve, collect(values(letters))...) && return letters
    end
end