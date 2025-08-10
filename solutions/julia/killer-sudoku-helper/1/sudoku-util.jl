function combinations_in_cage(maxValue::Int, count::Int, exclude::Union{AbstractArray{Int},Nothing}=nothing)
    output, combos = [], []
    maxN = maxValue > 9 ? 9 : maxValue
    lng = (maxN - count) + 1

    isnothing(exclude) && exclude = Array{Int}()

    for i in count:-1:0
        rng = i:i+lng
        push!(combos, rng[rng.|>n->!in(n, exclude)])
    end

	while true
		for i in combos
			for j in combos
				for k in j
				end
			end

		end

    return output
end