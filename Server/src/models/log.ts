import mongoose, { Document, Schema } from 'mongoose';

export interface ILog extends Document {
    temperature: number,
    air_humidity: number,
    soil_humidity: number,
    timestamp: Date,
    loggerId: Schema.Types.ObjectId
}

const LogSchema: Schema = new Schema(
    {
        temperature: {
            type: Number,
            required: true
        },
        air_humidity: Number,
        soil_humidity: Number,
        loggerId: { 
            type: Schema.Types.ObjectId,
            ref: 'Loggers'
        }
    },
    {
        timestamps: true
    }
);

export default mongoose.model<ILog>('Log', LogSchema);