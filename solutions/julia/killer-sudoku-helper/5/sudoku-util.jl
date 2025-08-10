function combinations_in_cage(sumV::Int, count::Int, exclude=nothing)
	exclude = isnothing(exclude) ? [] : exclude
    excludeStr = "[$(join(exclude))x]"
    global excludeRegex = Regex(".*$excludeStr.*")

    sn = parse(Int, "1" * repeat('1', count - 1))
    en = parse(Int, join(9-count+1:9))
    output = []

    combos = string.(sn:en)
    combos = combos[filterCombo.(combos)]

    for combo in combos
        nums = parse.(Int, split(combo, ""))
        if (sum(nums) == sumV)
            nums = sort(unique(nums))
            if !in(nums, output)
                push!(output, nums)
            end
        end
    end

    return output
end

function filterCombo(item::AbstractString)::Bool
    repeatedDigits = r".*(\d+).*\1.*"
    (contains(item, '0') || contains(item, repeatedDigits) || contains(item, excludeRegex)) == false
end