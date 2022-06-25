orders = ["wink","double blink","close your eyes","jump"]

processbit(bit) = (global i += 1;i == 5 && (global doreverse = bit == 1;return nothing);set_output(bit))
set_output(bit) = bit == 1 && (output[i] = orders[i])

function init()
    global output = ["","","",""]
    global doreverse = false
    global i = 0
end
    
function secret_handshake(code)
    init()
    processbit.(@view digits(code,base=2,pad=5)[1:5])
    global output = output[output .!= ""]
    return doreverse ? reverse(output) : output
end
