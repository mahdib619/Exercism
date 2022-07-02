function distance(a, b)
	length(a) != length(b) && throw(ArgumentError("inpus length is not equal!"))

	hammingDistance = 0
	for i in 1:length(a)
		hammingDistance += a[i] != b[i] ? 1 : 0
	end

	return hammingDistance
end
