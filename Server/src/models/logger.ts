import mongoose from 'mongoose';

const LoggerSchema = new mongoose.Schema({
    minimum_temperature: String,
    logs: [{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'Log'
    }],
    plants: [{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'Plant'
    }]
});

export default mongoose.model('Logger', LoggerSchema);