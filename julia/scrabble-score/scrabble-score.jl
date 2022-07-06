lettergroupvalues = Dict("AEIOULNRST" => 1, "DG" => 2, "BCMP" => 3, "FHVWY" => 4, "K" => 5, "JX" => 8, "QZ" => 10)
lettergroupmapper = Dict([c => g for g in keys(lettergroupvalues) for c in collect(g)])

getcharvalue(c) = (c = uppercase(c); haskey(lettergroupmapper, c) ? lettergroupvalues[lettergroupmapper[c]] : 0)
score(str) = sum(getcharvalue.(isempty(str) ? [' '] : collect(str)))