use unicode_segmentation::UnicodeSegmentation;

pub fn reverse(input: &str) -> String {
    let reverse_arr: Vec<String> = input
        .graphemes(true)
        .rev()
        .map(|ch| ch.to_string())
        .collect();
        
    reverse_arr.join("")
}
