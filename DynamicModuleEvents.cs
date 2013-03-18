
public static class EventHelper {

		/// <summary>
		/// Subscribe to events
		/// </summary>
		public static void SubscribeToEvents() {
		
		    // Subscribe to the DynamicModule events
		    EventHub.Subscribe<IDynamicContentCreatedEvent>(e => DynamicContentCreatedEvent(e));
		    EventHub.Subscribe<IDynamicContentUpdatedEvent>(e => DynamicContentUpdatedEvent(e));
		}
		
		/// <summary>
		/// Dynamics the content updated event_ handler.
		/// </summary>
		/// <param name="event">The event.</param>
		private static void DynamicContentCreatedEvent(IDynamicContentCreatedEvent eventInfo) {
		
		}
		
		/// <summary>
		/// Dynamics the content updated event_ handler.
		/// </summary>
		/// <param name="event">The event.</param>
		private static void DynamicContentUpdatedEvent(IDynamicContentUpdatedEvent eventInfo) {
		
		}
}
