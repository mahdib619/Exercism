function bob(stimulus)
	stimulus = rstrip(stimulus)
    nonlower = true
	hasletter = false
    for c in collect(stimulus)
		hasletter = !hasletter ? isletter(c) : true
        nonlower = nonlower ? !isletter(c) || isuppercase(c) : false
    end
	allupper = hasletter && nonlower

    if isempty(stimulus)
        "Fine. Be that way!"
    elseif endswith(stimulus, '?')
        allupper ? "Calm down, I know what I'm doing!" : "Sure."
    elseif allupper
        "Whoa, chill out!"
    else
        "Whatever."
    end
end