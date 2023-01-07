use std::collections::HashMap;

pub fn can_construct_note(magazine: &[&str], note: &[&str]) -> bool {
    let mut counter: HashMap<&str, i16> = HashMap::new();

    for word in magazine {
        *counter.entry(word).or_default() += 1;
    }

    for word in note {
        match counter.get_mut(word) {
            Some(v) => {
                if *v == 0 {
                    return false;
                } else {
                    *v -= 1
                }
            }
            None => {
                return false;
            }
        }
    }

    true
}
