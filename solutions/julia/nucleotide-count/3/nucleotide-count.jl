"""
    count_nucleotides(strand)

The count of each nucleotide within `strand` as a dictionary.

Invalid strands raise a `DomainError`.

"""
function count_nucleotides(strand)
    chars = length(strand) > 0 ? only.(split(strand, "")) : []
    nucleotides = ['A', 'C', 'T', 'G']
    if !isnothing(findfirst(c -> !in(c, nucleotides), chars))
        throw(DomainError(""))
    end

    countDic = Dict(c => 0 for c in nucleotides)
    for c in chars
        countDic[c] += 1
    end
    return countDic
end