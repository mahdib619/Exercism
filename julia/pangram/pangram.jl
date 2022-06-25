"""
    ispangram(input)

Return `true` if `input` contains every alphabetic character (case insensitive).

"""
function ispangram(input)
    if length(input) < 26
        return false
    end
    
    lInp = lowercase(input)
    asciis = Int.(only.(split(lInp,"")))
    return issubset(97:122,asciis)
end