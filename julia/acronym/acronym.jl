acronym(phrase) = uppercase(join(map(a -> a[1], split(replace(phrase, "-" => " ", "_" => "")))))