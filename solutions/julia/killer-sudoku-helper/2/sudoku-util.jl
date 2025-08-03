function combinations_in_cage(sumV::Int, count::Int, exclude=nothing)
    exclude = isnothing(exclude) ? [] : exclude

    sn = parse(Int, "1" * repeat('0', count - 1))
    en = parse(Int, repeat('9', count))

    output = []

    while sn <= en
        strSn = string(sn)
        if (length(strSn) == length(unique(strSn)))
            nums = parse.(Int, split(strSn, ""))
            if (!any(n -> in(exclude, n), nums) && sum(nums) == sumV)
                nums = sort(unique(nums))
                if !in(nums, output)
                    push!(output, nums)
                end
            end
        end

		sn += 1
    end

    return output
end