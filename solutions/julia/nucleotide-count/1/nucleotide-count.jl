"""
    count_nucleotides(strand)

The count of each nucleotide within `strand` as a dictionary.

Invalid strands raise a `DomainError`.

"""
function count_nucleotides(strand)
    chars = length(strand) > 0 ? only.(split(strand,"")) : []
    if !isnothing(findfirst(c->!in(c,['A','C','T','G']),chars))
        throw(DomainError(""))
    end
    
    countDic = Dict('A'=>0,'C'=>0,'G'=>0,'T'=>0)
    for c in chars
        count = get(countDic,c,1)
        countDic[c] = count + 1
    end
    return countDic
end
