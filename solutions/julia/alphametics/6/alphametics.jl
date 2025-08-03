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

println(
    solve(
        "THIS + A + FIRE + THEREFORE + FOR + ALL + HISTORIES + I + TELL + A + TALE + THAT + FALSIFIES + ITS + TITLE + TIS + A + LIE + THE + TALE + OF + THE + LAST + FIRE + HORSES + LATE + AFTER + THE + FIRST + FATHERS + FORESEE + THE + HORRORS + THE + LAST + FREE + TROLL + TERRIFIES + THE + HORSES + OF + FIRE + THE + TROLL + RESTS + AT + THE + HOLE + OF + LOSSES + IT + IS + THERE + THAT + SHE + STORES + ROLES + OF + LEATHERS + AFTER + SHE + SATISFIES + HER + HATE + OFF + THOSE + FEARS + A + TASTE + RISES + AS + SHE + HEARS + THE + LEAST + FAR + HORSE + THOSE + FAST + HORSES + THAT + FIRST + HEAR + THE + TROLL + FLEE + OFF + TO + THE + FOREST + THE + HORSES + THAT + ALERTS + RAISE + THE + STARES + OF + THE + OTHERS + AS + THE + TROLL + ASSAILS + AT + THE + TOTAL + SHIFT + HER + TEETH + TEAR + HOOF + OFF + TORSO + AS + THE + LAST + HORSE + FORFEITS + ITS + LIFE + THE + FIRST + FATHERS + HEAR + OF + THE + HORRORS + THEIR + FEARS + THAT + THE + FIRES + FOR + THEIR + FEASTS + ARREST + AS + THE + FIRST + FATHERS + RESETTLE + THE + LAST + OF + THE + FIRE + HORSES + THE + LAST + TROLL + HARASSES + THE + FOREST + HEART + FREE + AT + LAST + OF + THE + LAST + TROLL + ALL + OFFER + THEIR + FIRE + HEAT + TO + THE + ASSISTERS + FAR + OFF + THE + TROLL + FASTS + ITS + LIFE + SHORTER + AS + STARS + RISE + THE + HORSES + REST + SAFE + AFTER + ALL + SHARE + HOT + FISH + AS + THEIR + AFFILIATES + TAILOR + A + ROOFS + FOR + THEIR + SAFE == FORTRESSES",
    ),
)
