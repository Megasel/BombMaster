package com.supersonic.wisdom.library.data.repository;

import com.supersonic.wisdom.library.data.repository.datasource.EventMetadataLocalDataSource;
import com.supersonic.wisdom.library.domain.events.IEventMetadataRepository;

public class EventMetadataRepository implements IEventMetadataRepository {

    private EventMetadataLocalDataSource mDataSource;

    public EventMetadataRepository(EventMetadataLocalDataSource dataSource) {
        mDataSource = dataSource;
    }

    @Override
    public void put(String eventMetadataJson) {
        mDataSource.put(eventMetadataJson);
    }

    @Override
    public String get() {
        return mDataSource.get();
    }

    @Override
    public void update(String eventMetadataJson) {
        mDataSource.update(eventMetadataJson);
    }
}
