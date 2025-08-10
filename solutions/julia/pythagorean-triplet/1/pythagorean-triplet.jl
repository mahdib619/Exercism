function pythagorean_triplets(n)
    triplets = []
    for i = 1:n, j = i:n
        k = n - (i + j)
        k > j && i^2 + j^2 == k^2 && push!(triplets, (i, j, k))
    end
    triplets
end