function sieve(limit)
    marked = Set()
	
    for n in 2:limit
        n in marked && continue
        push!.((marked,), n+n:n:limit)
    end

    filter(n -> !in(n, marked), 2:limit)
end
