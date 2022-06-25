function combinations_in_cage(sumV::Int, count::Int, exclude=nothing)
    exclude = isnothing(exclude) ? [] : exclude
    excludeRegex = Regex(".*[$(join(exclude))x].*")

    sn = parse(Int, join(1:count))
    en = parse(Int, join(9-count+1:9))
    output = []

    combos = string.(sn:en)
    combos = combos[filterCombo.(combos, excludeRegex)]

    for combo in combos
        nums = parse.(Int, split(combo, ""))
        if (sum(nums) == sumV && !any(l -> any(i -> in(i, l), nums), output))
            push!(output, nums)
        end
    end

    return output
end

function filterCombo(item::AbstractString, excludeRegex::Regex)::Bool
    repeatedDigits = r".*(\d+).*\1.*"
    (contains(item, '0') || contains(item, excludeRegex) || contains(item, repeatedDigits)) == false
end