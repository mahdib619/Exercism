function bob(stimulus)
	stimulus = rstrip(stimulus)
    nonlower = allspace = true
	hasletter = false
    for c in collect(stimulus)
		hasletter = !hasletter ? isletter(c) : true
        allspace = allspace ? isspace(c) : false
        nonlower = nonlower ? !isletter(c) || isuppercase(c) : false
    end
	allupper = hasletter && nonlower

    if allspace
        "Fine. Be that way!"
    elseif endswith(stimulus, '?')
        allupper ? "Calm down, I know what I'm doing!" : "Sure."
    elseif allupper
        "Whoa, chill out!"
    else
        "Whatever."
    end
end