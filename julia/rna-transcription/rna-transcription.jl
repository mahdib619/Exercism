dic = Dict('G' => 'C', 'C' => 'G', 'T' => 'A', 'A' => 'U')
to_rna(dna) = join(tocomplement.(collect(dna)))
tocomplement(n) = haskey(dic, n) ? dic[n] : throw(ErrorException("is not a nucleotide!"))