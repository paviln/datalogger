import mongoose, { Document, Schema } from 'mongoose';

export interface ILogger extends Document {
    minimum_temperature: Number,
    logs: Schema.Types.ObjectId[],
    plants: Schema.Types.ObjectId[],
}

const LoggerSchema = new Schema({
    minimum_temperature: String,
    logs: [{
        type: Schema.Types.ObjectId,
        ref: 'Logs'
    }],
    plants: [{
        type: Schema.Types.ObjectId,
        ref: 'Plants'
    }]
});

export default mongoose.model<ILogger>('Loggers', LoggerSchema);