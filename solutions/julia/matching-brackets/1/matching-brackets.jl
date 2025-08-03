matches = Dict('}' => '{', ')' => '(', ']' => '[')

function matching_brackets(str)
    str = replace(str, r"(?![\[\]\{\}\(\)])." => "")

    bs = [' ']
    for c in collect(str)
        bs[end] == get(matches, c, nothing) ? pop!(bs) : push!(bs, c)
    end

	length(bs) == 1
end