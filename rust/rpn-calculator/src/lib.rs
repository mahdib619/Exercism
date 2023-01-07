#[derive(Debug)]
pub enum CalculatorInput {
    Add,
    Subtract,
    Multiply,
    Divide,
    Value(i32),
}

pub fn evaluate(inputs: &[CalculatorInput]) -> Option<i32> {
    let mut results: Vec<i32> = vec![];

    for inp in inputs {
        match inp {
            CalculatorInput::Value(v) => results.push(*v),
            _ => match do_operation(results.pop(), results.pop(), inp) {
                Some(v) => results.push(v),
                None => return None,
            },
        }
    }

    if results.len() == 1 {
        results.pop()
    } else {
        None
    }
}

fn do_operation(a: Option<i32>, b: Option<i32>, operator: &CalculatorInput) -> Option<i32> {
    if a.is_none() || b.is_none() {
        return None;
    }

    let (b, a) = (b.unwrap(), a.unwrap());

    match operator {
        CalculatorInput::Add => Some(b + a),
        CalculatorInput::Divide => Some(b / a),
        CalculatorInput::Multiply => Some(b * a),
        CalculatorInput::Subtract => Some(b - a),
        _ => None,
    }
}
