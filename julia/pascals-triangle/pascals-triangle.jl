function triangle(n)
    (n < 0 && throw(DomainError("")) || n == 0 && return [])

    output = [[1]]

    for i in 2:n
        c = push!(output, [1])[i]

        for j in 2:i-1
            push!(c, get(output[i-1], j - 1, 0) + get(output[i-1], j, 0))
        end

        push!(c, 1)
    end
    
    output
end
