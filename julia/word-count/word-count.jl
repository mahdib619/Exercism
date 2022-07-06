function wordcount(sentence)
    wordscount = Dict()
    sentence = lowercase(replace(replace(replace(sentence, r"[\n\t,]" => " "),  r"((?![\d\w' ]).)*" => ""),r"(^')|('$)|( ')|(' )" => " "))
    for w in split(sentence)
        c = get(wordscount, w, 0) + 1
        wordscount[w] = c
    end
    return wordscount
end