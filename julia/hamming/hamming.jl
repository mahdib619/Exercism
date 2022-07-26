function distance(a, b)
	length(a) != length(b) && throw(ArgumentError("inpus length is not equal!"))

	hammingDistance = 0
	for i in eachindex(a)
		hammingDistance += a[i] != b[i] ? 1 : 0
	end

	return hammingDistance
end
